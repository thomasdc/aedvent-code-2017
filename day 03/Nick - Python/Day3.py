
from math import sqrt

def CalculateMemory1(entries):
	length =  int(sqrt(entries) + 1)
	start_x = int(length/2 - 1)
	start_y = int(length/2 - 1)
	directions = [(1, 0), (0, -1), (-1, 0), (0, 1)]
	previous_direction = 3
	memory = [[0 for i in range(length)] for j in range(length)]
	x = start_x
	y = start_y
	for entry in range(1, entries+1):
		memory[x][y] = entry
		next_direction = directions[(previous_direction + 1) % len(directions)]
		if (memory[x  + next_direction[0]][y + next_direction[1]] == 0):
			x += next_direction[0]
			y += next_direction[1]
			previous_direction = (previous_direction + 1) % len(directions)
		else:
			x += directions[previous_direction][0]
			y += directions[previous_direction][1]
	position = [(ix,iy) for ix, row in enumerate(memory) for iy, i in enumerate(row) if i == entries]
	print(abs(start_x - position[0][0]) + abs(start_y - position[0][1]))


def CalculateMemory2(entries):
	length =  int(sqrt(entries) + 1)
	start_x = int(length/2 - 1)
	start_y = int(length/2 - 1)
	directions = [(1, 0), (0, -1), (-1, 0), (0, 1)]
	previous_direction = 3
	memory = [[0 for i in range(length)] for j in range(length)]
	x = start_x
	y = start_y
	memory[x][y] = 1
	for entry in range(1, entries+1):
		next_direction = directions[(previous_direction + 1) % len(directions)]
		if (memory[x  + next_direction[0]][y + next_direction[1]] == 0):
			x += next_direction[0]
			y += next_direction[1]
			previous_direction = (previous_direction + 1) % len(directions)
		else:
			x += directions[previous_direction][0]
			y += directions[previous_direction][1]	
		memory[x][y] = CalculateValue(memory, x, y)
		if memory[x][y] > entries:
			print(memory[x][y])
			break
	
def CalculateValue(memory, x, y):
	sum = 0
	for i in range(x - 1, x + 2):
		for j in range(y - 1, y + 2):
			if i >= 0 and j >= 0 and i < len(memory) and j < len(memory[0]):
				sum += memory[i][j]
	return sum

memory = CalculateMemory2(277678)
