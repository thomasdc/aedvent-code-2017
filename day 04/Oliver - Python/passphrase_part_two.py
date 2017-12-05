"""
--- Part Two ---

For added security, yet another system policy has been put in place. Now, a valid passphrase must contain no two words that are anagrams of each other - that is, a passphrase is invalid if any word's letters can be rearranged to form any other word in the passphrase.

For example:

abcde fghij is a valid passphrase.
abcde xyz ecdab is not valid - the letters from the third word can be rearranged to form the first word.
a ab abc abd abf abj is a valid passphrase, because all letters need to be used when forming another word.
iiii oiii ooii oooi oooo is valid.
oiii ioii iioi iiio is not valid - any of these words can be rearranged to form any other word.
Under this new system policy, how many passphrases are valid?

"""
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

