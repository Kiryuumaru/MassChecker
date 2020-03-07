import os, struct, socket
from gpiozero import LED
from time import sleep
from picamera import PiCamera
from multiprocessing import Process
import socket
import cv2

HOST = ""
PORT = 8000
PORT_CALLBACK = 8001

def udpCallback():
    while True:
        try:
            print("UDP socket awaiting messages...")
            r = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
            r.bind((HOST, PORT_CALLBACK))
            
            while True:
                data, addr = r.recvfrom(1024)
                print("from: ", addr, "msg: ", data.decode())
                if data.decode() == "PING":
                    r.sendto(("PONG").encode(), addr)
                    
        except Exception as e:
            print(e)
        
        sleep(2)
    
def tcpCallback():
    while True:
        try:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            s.bind((HOST, PORT))
            s.listen(1)
            print("TCP socket awaiting connection...")
            (conn, addr) = s.accept()
            
            conn.send(("OKENDOFMSG").encode())
            print("Socket connected")
            
            while True:
                string_recv = (conn.recv(1024)).decode()
                print("Recv: " + string_recv)
                if string_recv == "PINGENDOFMSG":
                    conn.send(("PONGENDOFMSG").encode())
                elif string_recv == "NEXTENDOFMSG":
                    printer = LED(26)
                    printer.on()
                    printer.close()
                elif string_recv == "SCANENDOFMSG":
                    print_paper()
                    with open('/home/pi/Desktop/Checker/image_in.jpg', 'rb') as file:
                        data = file.read()
                    message = data + ("ENDOFMSG").encode()
                    print('Sending data to:', addr)
                    conn.sendall(message)
                elif string_recv == "EXENDOFMSG":
                    conn.close()
                    break
                elif string_recv == "SHUTENDOFMSG":
                    conn.close()
                    quit()
                print("Done")
     
        except Exception as e:
            print(e)
        
        sleep(2)
    
def print_paper():
    camera = PiCamera()
    sleep(2)
    camera.capture('/home/pi/Desktop/Checker/image_in.jpg')
    camera.close()
    image = cv2.imread('/home/pi/Desktop/Checker/image_in.jpg')
    #dim = (int(900), int(720)) 
    #image = cv2.resize(image, dim, interpolation = cv2.INTER_AREA)
    image_save = cv2.rotate(image, cv2.ROTATE_90_COUNTERCLOCKWISE)
    cv2.imwrite('/home/pi/Desktop/Checker/image_in.jpg', image_save)

if __name__ == '__main__':
    #print_paper()
    p1 = Process(target=tcpCallback)
    p1.start()
    p2 = Process(target=udpCallback)
    p2.start()
    p1.join()
    p2.join()

