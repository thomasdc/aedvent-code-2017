table = [int(line.rstrip('\n')) for line in open('day5.txt')]
steps = 0
position = 0
while position >= 0 and position < len(table):
	steps +=1
	old_position = position
	position += table[position]
	if table[old_position] >= 3:
		table[old_position] -= 1
	else:
		table[old_position] += 1
print(steps)