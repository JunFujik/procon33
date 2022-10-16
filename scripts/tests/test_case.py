import subprocess
import os
import pathlib
def solve(filename,filesize):
  cmd = ["python3","search_part.py",filename,"--bench","-n",filesize]#"--flatten",]
  subprocess.check_call(cmd)
  subprocess.check_call(["rm",filename])
def main():
  f = open("data.txt","r")
  data = f.readlines()
  size = len(data)
  for i in range(size):
    data[i] = data[i][:-1]
  solve(data[0],str(size-1))
  f = open("result.txt","r")
  result = f.readlines()
  f.close();
  for i in range(size-1):
    result[i] = result[i][:-1]
  f = open("all_result.txt","a")
  f.writelines(str(len(set(data)&set(result)))+"\n")
  f.close()
  subprocess.check_call(["rm","data.txt"])
  subprocess.check_call(["rm","result.txt"])
if __name__ == "__main__":
  main()
