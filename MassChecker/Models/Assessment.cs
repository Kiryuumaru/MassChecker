using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MassChecker.Services;

namespace MassChecker.Models
{
    #region Enums

    public enum AssessmentType
    {
        Item10, Item20, Item30, Item40, Item50, Item60, Item70, Item80, Item90, Item100
    }
    public enum AssessmentSet
    {
        SetA, SetB, SetC
    }
    public enum Key { A, B, C, D, E }

    #endregion

    #region AssessmentItem

    public class AssessmentItem
    {
        #region ArraySerializer

        public static AssessmentItem[] Clone(AssessmentItem[] assessmentItems)
        {
            var newArray = new AssessmentItem[assessmentItems.Length];
            for (var i = 0; i < assessmentItems.Length; i++)
                newArray[i] = new AssessmentItem(assessmentItems[i].Key, assessmentItems[i].Points);
            return newArray;
        }

        public static string EncodeArray(AssessmentItem[] items)
        {
            string Data = "";
            for (int i = 0; i < items.Length; i++) { Data += items[i].Data; }
            return Data;
        }

        public static AssessmentItem[] DecodeArray(string Data)
        {
            List<AssessmentItem> items = new List<AssessmentItem>();
            int lastIndex = 0;
            while (lastIndex <= Data.Length - 4)
            {
                items.Add(new AssessmentItem(Data.Substring(lastIndex, 4)));
                lastIndex += 4;
            }
            return items.ToArray();
        }

        public static bool Compare(AssessmentItem[] left, AssessmentItem[] right)
        {
            if (left.Length != right.Length) return false;
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i].Key != right[i].Key || left[i].Points != right[i].Points) return false;
            }
            return true;
        }

        public static AssessmentItem[] MakeCopy(AssessmentItem[] assessmentItems)
        {
            AssessmentItem[] ret = new AssessmentItem[assessmentItems.Length];
            for (int i = 0; i < assessmentItems.Length; i++)
            {
                ret[i] = new AssessmentItem(assessmentItems[i].Key, assessmentItems[i].Points);
            }
            return ret;
        }

        #endregion

        #region Properties

        private string Data = "";

        public Key Key
        {
            get
            {
                if (Data[0].Equals('1')) return Key.A;
                else if (Data[0].Equals('2')) return Key.B;
                else if (Data[0].Equals('3')) return Key.C;
                else if (Data[0].Equals('4')) return Key.D;
                else if (Data[0].Equals('5')) return Key.E;
                else throw new Exception("Deserialize error");
            }
            set
            {
                switch (value)
                {
                    case Key.A:
                        Data = "1" + Data.Substring(1);
                        break;
                    case Key.B:
                        Data = "2" + Data.Substring(1);
                        break;
                    case Key.C:
                        Data = "3" + Data.Substring(1);
                        break;
                    case Key.D:
                        Data = "4" + Data.Substring(1);
                        break;
                    case Key.E:
                        Data = "5" + Data.Substring(1);
                        break;
                }
            }
        }
        public int Points
        {
            get
            {
                return Convert.ToInt32(Data.Substring(1));
            }
            set
            {
                Data = Data[0] + value.ToString("000");
            }
        }

        #endregion

        #region Constructors

        public AssessmentItem(Key key, int points = 1)
        {
            Data = "1001"; // Default Ans A and Points 001
            Key = key;
            Points = points;
        }

        public AssessmentItem(string data)
        {
            Data = data;
        }

        #endregion

        #region Methods



        #endregion
    }

    #endregion

    #region AssessmentItemParser

    public class AssessmentItemParser
    {
        #region Properties

        public AssessmentItem[] AssessmentItems { get; private set; }
        public List<int> UnshadedItems { get; private set; }
        public List<int> MultishadedItems { get; private set; }
        public bool IsValid { get; private set; }

        #endregion

        #region Initializers

        private AssessmentItemParser()
        {

        }

        #endregion

        #region Statics

        public static AssessmentItemParser Parse(PaperItem[] paperItems)
        {
            AssessmentItemParser itemParser = new AssessmentItemParser
            {
                AssessmentItems = new AssessmentItem[paperItems.Length],
                UnshadedItems = new List<int>(),
                MultishadedItems = new List<int>(),
                IsValid = true
            };
            for (int i = 0; i < paperItems.Length; i++)
            {
                int ansNum = 0;
                ansNum += paperItems[i].A ? 1 : 0;
                ansNum += paperItems[i].B ? 1 : 0;
                ansNum += paperItems[i].C ? 1 : 0;
                ansNum += paperItems[i].D ? 1 : 0;
                ansNum += paperItems[i].E ? 1 : 0;
                if (ansNum == 0)
                {
                    itemParser.UnshadedItems.Add(i + 1);
                }
                if (ansNum > 1)
                {
                    itemParser.MultishadedItems.Add(i + 1);
                }
                else
                {
                    if (paperItems[i].A) itemParser.AssessmentItems[i] = new AssessmentItem(Key.A);
                    else if (paperItems[i].B) itemParser.AssessmentItems[i] = new AssessmentItem(Key.B);
                    else if (paperItems[i].C) itemParser.AssessmentItems[i] = new AssessmentItem(Key.C);
                    else if (paperItems[i].D) itemParser.AssessmentItems[i] = new AssessmentItem(Key.D);
                    else if (paperItems[i].E) itemParser.AssessmentItems[i] = new AssessmentItem(Key.E);
                }
            }
            return itemParser;
        }

        #endregion
    }

    #endregion

    public class Assessment
    {
        #region Statics

        public static AssessmentItem[] CreateItems(AssessmentType assessmentType)
        {
            int count = 0;
            switch (assessmentType)
            {
                case AssessmentType.Item10:
                    count = 10;
                    break;
                case AssessmentType.Item20:
                    count = 20;
                    break;
                case AssessmentType.Item30:
                    count = 30;
                    break;
                case AssessmentType.Item40:
                    count = 40;
                    break;
                case AssessmentType.Item50:
                    count = 50;
                    break;
                case AssessmentType.Item60:
                    count = 60;
                    break;
                case AssessmentType.Item70:
                    count = 70;
                    break;
                case AssessmentType.Item80:
                    count = 80;
                    break;
                case AssessmentType.Item90:
                    count = 90;
                    break;
                case AssessmentType.Item100:
                    count = 100;
                    break;
            }
            AssessmentItem[] items = new AssessmentItem[count];
            for (int i = 0; i < count; i++) { items[i] = new AssessmentItem(Key.A); }
            return items;
        }

        public static int GetTypeLength(AssessmentType type)
        {
            switch (type)
            {
                case AssessmentType.Item10:
                    return 10;
                case AssessmentType.Item20:
                    return 20;
                case AssessmentType.Item30:
                    return 30;
                case AssessmentType.Item40:
                    return 40;
                case AssessmentType.Item50:
                    return 50;
                case AssessmentType.Item60:
                    return 60;
                case AssessmentType.Item70:
                    return 70;
                case AssessmentType.Item80:
                    return 80;
                case AssessmentType.Item90:
                    return 90;
                case AssessmentType.Item100:
                    return 100;
                default:
                    return 0;
            }
        }

        #endregion

        #region Properties

        public AssessmentType AssessmentType { get; set; }
        public AssessmentItem[] SetAKeys { get; set; }
        public AssessmentItem[] SetBKeys { get; set; }
        public AssessmentItem[] SetCKeys { get; set; }
        public bool SetBEnabled { get; set; }
        public bool SetCEnabled { get; set; }
        public double PassingRate { get; set; }

        #endregion

        #region Constructors

        public Assessment(AssessmentType type, double passingRate)
        {
            AssessmentType = type;
            SetAKeys = CreateItems(type);
            SetBKeys = CreateItems(type);
            SetCKeys = CreateItems(type);
            SetBEnabled = false;
            SetCEnabled = false;
            PassingRate = passingRate;
        }

        public Assessment(string blob)
        {
            switch (Extension.Helpers.GetValue(blob, "0000"))
            {
                case "010":
                    AssessmentType = AssessmentType.Item10;
                    break;
                case "020":
                    AssessmentType = AssessmentType.Item20;
                    break;
                case "030":
                    AssessmentType = AssessmentType.Item30;
                    break;
                case "040":
                    AssessmentType = AssessmentType.Item40;
                    break;
                case "050":
                    AssessmentType = AssessmentType.Item50;
                    break;
                case "060":
                    AssessmentType = AssessmentType.Item60;
                    break;
                case "070":
                    AssessmentType = AssessmentType.Item70;
                    break;
                case "080":
                    AssessmentType = AssessmentType.Item80;
                    break;
                case "090":
                    AssessmentType = AssessmentType.Item90;
                    break;
                case "100":
                    AssessmentType = AssessmentType.Item100;
                    break;
            }
            SetAKeys = AssessmentItem.DecodeArray(Extension.Helpers.GetValue(blob, "0001"));
            SetBKeys = AssessmentItem.DecodeArray(Extension.Helpers.GetValue(blob, "0002"));
            SetCKeys = AssessmentItem.DecodeArray(Extension.Helpers.GetValue(blob, "0003"));
            SetCEnabled = Extension.Helpers.GetValue(blob, "0004").Equals("1");
            SetBEnabled = Extension.Helpers.GetValue(blob, "0005").Equals("1");
            PassingRate = Convert.ToDouble(Extension.Helpers.GetValue(blob, "0006"));
        }

        #endregion

        #region Methods

        public AssessmentItem[] GetItems(AssessmentSet set)
        {
            switch (set)
            {
                case AssessmentSet.SetA:
                    return SetAKeys;
                case AssessmentSet.SetB:
                    return SetBKeys;
                case AssessmentSet.SetC:
                    return SetCKeys;
                default:
                    return null;
            }
        }

        public bool GetEnabled(AssessmentSet set)
        {
            switch (set)
            {
                case AssessmentSet.SetB:
                    return SetBEnabled;
                case AssessmentSet.SetC:
                    return SetCEnabled;
                default:
                    return true;
            }
        }

        public void SetItems(AssessmentItem[] items, AssessmentSet set)
        {
            switch (set)
            {
                case AssessmentSet.SetA:
                    SetAKeys = items;
                    break;
                case AssessmentSet.SetB:
                    SetBKeys = items;
                    break;
                case AssessmentSet.SetC:
                    SetCKeys = items;
                    break;
            }
        }

        public string GetFormattedEnabledSetNames()
        {
            string names = "Set A";
            if (SetBEnabled)
            {
                names += ", Set B";
            }
            if (SetCEnabled)
            {
                names += ", Set C";
            }
            return names;
        }

        public string GetFormattedAssessmentType()
        {
            string assessmentType = "";
            switch (AssessmentType)
            {
                case AssessmentType.Item10:
                    assessmentType = "10 Items";
                    break;
                case AssessmentType.Item20:
                    assessmentType = "20 Items";
                    break;
                case AssessmentType.Item30:
                    assessmentType = "30 Items";
                    break;
                case AssessmentType.Item40:
                    assessmentType = "40 Items";
                    break;
                case AssessmentType.Item50:
                    assessmentType = "50 Items";
                    break;
                case AssessmentType.Item60:
                    assessmentType = "60 Items";
                    break;
                case AssessmentType.Item70:
                    assessmentType = "70 Items";
                    break;
                case AssessmentType.Item80:
                    assessmentType = "80 Items";
                    break;
                case AssessmentType.Item90:
                    assessmentType = "90 Items";
                    break;
                case AssessmentType.Item100:
                    assessmentType = "100 Items";
                    break;
            }
            return assessmentType;
        }

        public string Bloberize()
        {
            string blob = "";
            string assessmentType = "";
            switch (AssessmentType)
            {
                case AssessmentType.Item10:
                    assessmentType = "010";
                    break;
                case AssessmentType.Item20:
                    assessmentType = "020";
                    break;
                case AssessmentType.Item30:
                    assessmentType = "030";
                    break;
                case AssessmentType.Item40:
                    assessmentType = "040";
                    break;
                case AssessmentType.Item50:
                    assessmentType = "050";
                    break;
                case AssessmentType.Item60:
                    assessmentType = "060";
                    break;
                case AssessmentType.Item70:
                    assessmentType = "070";
                    break;
                case AssessmentType.Item80:
                    assessmentType = "080";
                    break;
                case AssessmentType.Item90:
                    assessmentType = "090";
                    break;
                case AssessmentType.Item100:
                    assessmentType = "100";
                    break;
            }
            blob = Extension.Helpers.SetValue(blob, "0000", assessmentType);
            blob = Extension.Helpers.SetValue(blob, "0001", AssessmentItem.EncodeArray(SetAKeys));
            blob = Extension.Helpers.SetValue(blob, "0002", AssessmentItem.EncodeArray(SetBKeys));
            blob = Extension.Helpers.SetValue(blob, "0003", AssessmentItem.EncodeArray(SetCKeys));
            blob = Extension.Helpers.SetValue(blob, "0004", SetBEnabled ? "1" : "0");
            blob = Extension.Helpers.SetValue(blob, "0005", SetCEnabled ? "1" : "0");
            blob = Extension.Helpers.SetValue(blob, "0006", PassingRate.ToString());
            return blob;
        }

        #endregion
    }
}
