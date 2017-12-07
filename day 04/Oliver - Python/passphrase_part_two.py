
import itertools
def read_input(path):
    with open(path, "r") as a:
        return [line.split() for line in a]


def compare(phrases):
    result = []
    for line in phrases:
        temp = []
        for word1, word2 in itertools.combinations(line, 2):
            temp.append((sorted(word1) == sorted(word2)))
        result.append(not(max(temp)))
    return result

# Test example
test = read_input("day 04/Oliver - Python/test.txt")
compare(test)

# On input.txt
passphrases = read_input("day 04/Oliver - Python/input.txt")
result = compare(passphrases)
len(result)
# Valid results
print(sum(result))

