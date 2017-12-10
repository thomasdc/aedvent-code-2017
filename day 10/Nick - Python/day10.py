from functools import reduce

def reverse_array(array, current_position, step):
	subarray = []
	for x in range(step):
		subarray.append(array[(current_position + x) % 256])
	subarray.reverse()
	for x in range(step):
		array[(current_position + x) % 256] = subarray[x]
	return array

def reverse(array, line, current_position, skip_size):
	for step in line:
		reverse_array(array, current_position, step)
		current_position = (current_position + skip_size + step) % len(array)
		skip_size += 1
	return array, current_position, skip_size

def part1():
	lines = [line.rstrip('\n').split(',') for line in open('day10.txt')]
	line = [int(x) for x in lines[0]]
	numbers = [x for x in range(0,256)]
	current_position = 0
	skip_size = 0
	numbers, current_position, skip_size = reverse(numbers, line, current_position, skip_size)
	print(numbers[0]*numbers[1])

def part2():
	lines = [line.rstrip('\n') for line in open('day10.txt')]
	line = [ord(str(x)) for x in lines[0]] + [17, 31, 73, 47, 23]
	numbers = [x for x in range(0,256)]
	current_position = 0
	skip_size = 0
	for i in range(64):
		numbers, current_position, skip_size = reverse(numbers, line, current_position, skip_size)		
	dense = ''
	denselist = []
	for i in range(0,16):
		denselist.append('%02x'%reduce((lambda x,y: x ^ y), numbers[i*16:(i+1)*16]))
	print(''.join(denselist))

part1()
part2()
