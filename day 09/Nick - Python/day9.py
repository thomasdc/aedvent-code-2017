import re

lines = [line.rstrip('\n') for line in open('day9.txt')]
sumall = 0
sumgarbage = 0
for line in lines:
	line = re.sub(r'\!.','', line)
	original_line = line
	garbage = re.findall(r'\<.*?\>', line)
	line = re.sub(r'\<.*?\>','', line)
	sumgarbage += len(original_line) - len(line) - 2*len(garbage)
	totalsum = 0
	index = 0
	for x in line.split(','):
		opened = x.count('{')
		closed = x.count('}')
		totalsum += sum(range(1 + index, opened + index + 1)) 
		index += opened - closed
	sumall += totalsum
print(sumall)
print(sumgarbage)

