import numpy as np
from functools import reduce

def hash(elements, puzzle):
    position = 0
    skip = 0
    for x in puzzle:
        # Define index (start stop, with length from length_sequence) to take subset selection of the sequence
        start_pos = position
        stop_pos = position + x
        subseq = [n % len(elements) for n in range(start_pos, stop_pos)]
        # Take reverse sequence based on the subset
        reverse_arr = elements[subseq][::-1]
        elements[subseq] = reverse_arr
        # Increment position and skip size
        position += (x + skip) % len(elements)
        skip += 1
    return elements[0] * elements[1]

elements = np.array([0, 1, 2, 3, 4])
puzzle = [3, 4, 1, 5]

elements = np.array(range(0,256))
puzzle = [97,167,54,178,2,11,209,174,119,248,254,0,255,1,64,190]
hash(elements, puzzle)

# Part two
def reverse(elements, puzzle_input, position, skip):
    for x in puzzle_input:
        # Define index (start stop, with length from length_sequence) to take subset selection of the sequence
        start_pos = position
        stop_pos = position + x
        subseq = [n % len(elements) for n in range(start_pos, stop_pos)]
        # Take reverse sequence based on the subset
        reverse_arr = elements[subseq][::-1]
        elements[subseq] = reverse_arr
        # Increment position and skip size
        position += (x + skip) % len(elements)
        skip += 1
    return position, skip, elements

def sparse_hash(puzzle):
    elements = np.array(range(0, 256))
    rounds = 64
    puzzle_input = sum([[ord(x) for x in str(el)] for el in puzzle], []) + [17, 31, 73, 47, 23]
    position = 0
    skip = 0
    for _ in range(rounds):
        position, skip, elements = reverse(elements, puzzle_input, position, skip)
    hash_block = [elements[i:i + 16] for i in range(0, len(elements), 16)]
    dense_hash  = [reduce(lambda x, y: x ^ y, block) for block in hash_block]
    hash_as_string = ''.join('%02x'%block for block in dense_hash)
    print(str(hash_as_string))

puzzle = '97,167,54,178,2,11,209,174,119,248,254,0,255,1,64,190'
sparse_hash(puzzle)
sparse_hash("") == 'a2582a3a0e66e6e86e3812dcb672a272'
