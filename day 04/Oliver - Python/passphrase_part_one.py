"""
--- Day 4: High-Entropy Passphrases ---

A new system policy has been put in place that requires all accounts to use a passphrase instead of simply a password. A passphrase consists of a series of words (lowercase letters) separated by spaces.

To ensure security, a valid passphrase must contain no duplicate words.

For example:

aa bb cc dd ee is valid.
aa bb cc dd aa is not valid - the word aa appears more than once.
aa bb cc dd aaa is valid - aa and aaa count as different words.
The system's full passphrase list is available as your puzzle input. How many passphrases are valid?

"""
# read txt lines in
def read_input(path):
    with open(path, "r") as a:
        return [line.split() for line in a]

# logic to define valid phrases
def count_valid(phrases):
    orig_list = []
    unique_list = []
    for line in phrases:
        # Length of original passphrases
        orig_list.append(len(line))
        # Length of unique passphrases
        unique_list.append(len(set(line)))

    # Subset original list if no valid pass are found
    # Then print length of this subset list
    nr_valid = len([x for x,y in zip(orig_list, unique_list) if x == y ])
    return nr_valid

passphrases = read_input("day 04/Oliver - Python/input.txt")
count_valid(passphrases)

