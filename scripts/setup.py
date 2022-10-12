import subprocess
import os
import random
import pathlib
import datetime 
def init():
  res = subprocess.check_output(["curl","-OL","http://www.procon.gr.jp/wp-content/uploads//2022/05/JKspeech-v_1_0.zip"])
  res = subprocess.check_output(["unzip","JKspeech-v_1_0.zip"])
  res = subprocess.check_output(["rm","JKspeech-v_1_0.zip"])
  res = subprocess.check_output(["rm","E??*.wav"])
def main():
  if(not os.path.isdir("JKspeech")):
    init()
  num = 5
  #42から重複なしでnum個選ぶ(最大20個)
  st =set() 
  while(len(st)!=num):
    x = random.randrange(1,43)
    y = ""
    if(x<10):y="0"+str(x)
    else:y = str(x)
    st.add(y)
  file_name = "".join(str(datetime.datetime.now()).split(" "))+".wav"
  code = ''
  #音声ファイルを合成する
  file = open("data.txt","w")
  file.write(file_name+"\n")
  for i in st:
    file.write("J"+i+"\n")
    code+="J"+i+",1,24000,48000,"
  #実行する部分
  res = subprocess.check_output(["python3","makequal.py",code[:-1],"-o",file_name])
  file.close()
if __name__ == "__main__":
  main()
