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

