import argparse
import requests
import json
import os
import pathlib

def Match(HOST,TOKEN):
  res = requests.get(HOST+"match?token="+TOKEN)
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif(res.status_code == 400):
    print(f"result={res.text}")
    exit()
  elif(res.status_code != 200):
    print("result=error")
    exit()
    
  print("result=success", end=";")
  data_json = json.loads(res.text)
  for i in data_json:
    print(i,data_json[i],end=";",sep="=")

def problem(HOST,TOKEN):
  res = requests.get(HOST+"problem?token="+TOKEN)
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif(res.status_code == 400):
    print(f"result={res.text}")
    exit()
  elif(res.status_code != 200):
    print("result=error")
    exit()

  print("result=success", end=";")
  data_json = json.loads(res.text)
  for i in data_json:
    print(i,data_json[i],end=";",sep="=")

def chunk(num,HOST,TOKEN):
  #相対パスを返すように変更
  res = requests.post(HOST+"problem/chunks?n="+str(num)+"&token="+TOKEN)
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif(res.status_code == 400):
    print(f"result={res.text}")
    exit()
  elif(res.status_code != 200):
    print("result=error")
    exit()
  
  data_json = json.loads(res.text)
  filenames = [x for x in data_json["chunks"]]
  print("result=success;chunks=%s" % ",".join(filenames))


def answer(data,problem_id,HOST,TOKEN):
  url = HOST+"problem"+"?token="+TOKEN
  res = requests.post(url,json={"problem_id": problem_id,"answers": data})
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif res.status_code == 400:
    print(f"result={res.text}")
    exit()
  elif res.status_code != 200:
    print("result=error")
    exit()

  print("result=success;", end="")
  data_json = json.loads(res.text)
  for i in data_json:
    print(i,data_json[i],end=";",sep="=")

def get(filename,HOST,TOKEN):
  res = requests.get(HOST+"problem/chunks/"+filename+"?token="+TOKEN)
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif(res.status_code == 400):
    print(f"result={res.text}")
    exit()
  elif(res.status_code != 200):
    print("result=error")
    exit()
  
  url_data = res.content
  wav_path = "data/" + filename
  with open(wav_path,mode="wb") as f:
    f.write(url_data)
  print("result=success;path=%s" % str(pathlib.Path(wav_path).absolute()))
  
def test(HOST, TOKEN):
  res = requests.get(HOST + "test" + "?token=" + TOKEN)
  if(res.status_code == 502):
    print("result=BadGateway")
    exit()
  elif(res.status_code == 400):
    print(f"result={res.text}")
    exit()
  elif(res.status_code != 200):
    print("result=error")
    exit()
    
  body = res.content
  if body != b"OK":
    print("result=error")
    exit()
    
  print("result=success")

def main():
  parser = argparse.ArgumentParser(description="高専プロコン通信用のプログラム")
  parser.add_argument("type", help="(match,problem,chunk,answer)の内どのオペレーションか")
  parser.add_argument("--num", help="chunkを選んだ場合いくつ持ってくるか")
  parser.add_argument("--data", nargs='*',help="answerを指定したときどのデータを出すか")
  parser.add_argument("--token",help="tokenを指定してください")
  parser.add_argument("--host",help="hostを指定してください")
  parser.add_argument("--filename",help="欲しいファイルの名前を指定して下さい")
  parser.add_argument("--problem",help="解答する問題を指定してください")
  args = parser.parse_args()
  if(args.token==None):
    exit("No token in argument")
  if(args.host==None):
    exit("No host in argument")
  HOST = args.host
  TOKEN =args.token
  if(args.type=="match"):
    Match(HOST,TOKEN)
  elif(args.type=="problem"):
    problem(HOST,TOKEN)
  elif(args.type=="chunk"):
    chunk(args.num,HOST,TOKEN)
  elif(args.type=="answer"):
    answer(args.data,args.problem,HOST,TOKEN)
  elif(args.type=="get"):
    get(args.filename,HOST,TOKEN)
  elif args.type=="test":
    test(HOST, TOKEN)
  else:
    exit("No such instruction found")
if(__name__=="__main__"):
  if(not os.path.isdir("data")):
    os.makedirs("data")
  main()
