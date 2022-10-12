import statistics
import math
f = open('all_result.txt','r',encoding='UTF-8')
data = f.readlines()
f.close()
values = {}
data2 =[]
for i in data:
  i = i[:-1]
  if(not i.isdigit()):continue
  data2.append(int(i))
  if(i in values):
    values[i]+=1
  else:
    values[i] = 1
Sum = 0
print("それぞれの値")
for i in values:
  print(i,":",values[i])
  Sum+=(int(i)*values[i])
print("算術平均",statistics.mean(data2))
print("標準偏差",statistics.stdev(data2))
print("最大値",max(data2))
print("最小値",min(data2))
print("最頻値",statistics.mode(data2))
