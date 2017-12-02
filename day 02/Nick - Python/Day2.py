table = [line.rstrip('\n') for line in open('day2.txt')]
sum = 0
for row in table:
	sum += int(max([int(x) for x in row.split()]) - min([int(x) for x in row.split()]))
print(sum)

sum = 0
for row in table:
	dividable = [int(x) for x in row.split() if x in [x for y in row.split() if (int(x) % int(y) == 0 or int(y) % int(x) == 0) and x != y]]
	sum += int(max(dividable) / min(dividable))
print(sum)
