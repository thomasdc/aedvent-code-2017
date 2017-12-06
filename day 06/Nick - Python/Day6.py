lines = [line.rstrip('\n') for line in open('day6.txt')]
line = [int(x) for x in lines[0].split('\t')]
print(line)
line = [0,2,7,0]
history = []
while 1:
	index = 1
	maximum = max(line)
	max_position = line.index(maximum)
	line[(max_position)] = 0 
	while int(maximum) > 0:
		line[(max_position + index)%len(line)] = int(line[(max_position + index)%len(line)]) + 1 
		maximum = int(maximum) - 1
		index += 1
	if line in history:
		break;
	history.append(line[:])
print(len(history) - history.index(line))



