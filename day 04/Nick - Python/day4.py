
import collections
from itertools import groupby
lines = [line.rstrip('\n') for line in open('day4.txt')]

sum = 0
for line in lines:
	splitted = line.split()
	duplicates = [item for item, count in collections.Counter(splitted).items() if count > 1] #part 1
	groupedanagrams = [list(group) for key,group in groupby(sorted(splitted,key=sorted),sorted)] #part 2
	valid = 1
	for anagram in groupedanagrams:
		if len(anagram) > 1:
			valid = 0
	if valid:
		sum += 1
print(sum)