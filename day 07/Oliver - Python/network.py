from itertools import combinations

def root(path):
    # read file
    input_txt = [line.strip() for line in open(path)]
    split_txt = [line.replace(',','').split(' ') for line in input_txt]
    result = {first[0]: 0 for first in split_txt}
    for elone in split_txt:
        if any(elone[0] in item[1:] for item in split_txt):
            result[elone[0]] += 1
    print(dict((v,k) for k,v in result.items()).get(0))

root('day 07/Oliver - Python/input.txt')