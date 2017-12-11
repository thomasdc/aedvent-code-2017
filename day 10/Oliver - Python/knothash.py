
"""
To achieve this, begin with a list of numbers from 0 to 255,
- a current position which begins at 0 (the first element in the list),
- a skip size (which starts at 0),
- and a sequence of lengths (your puzzle input). Then, for each length:

Reverse the order of that length of elements in the list, starting with the element at the current position.
Move the current position forward by that length plus the skip size.
Increase the skip size by one.
"""
import numpy as np

length = list(range(0,256))
sequence = np.array([97,167,54,178,2,11,209,174,119,248,254,0,255,1,64,190])

length = [3, 4, 1, 5]
sequence = np.array([0, 1, 2, 3, 4])

position = 0
skip = 0
l = len(sequence)

for x in length:
    # Define index (start stop, with length from length_sequence) to take subset selection of the sequence
    start_pos = position % l
    stop_pos = (position + x ) % l
    # Logic to cycle the subset list over the sequence list
    if start_pos >=  stop_pos and x > 1:
        subseq = list(range(start_pos, l)) + list(range(0, stop_pos))
    elif x == 0:
        subseq = [start_pos]
    else:
        subseq = list(range(start_pos, stop_pos))
    print("start", "stop", "subseq")
    print(start_pos, stop_pos, subseq)
    # Take reverse sequence based on the subset
    reverse_arr = sequence[subseq][::-1]
    print(sequence[subseq],sequence[subseq][::-1], sequence)
    sequence[subseq] = reverse_arr
    print(sequence)
    # Increment position and skip size

    position += (x + skip) % l
    print(x + skip)
    skip += 1

    print(position, skip)


sequence[0]*sequence[1]
