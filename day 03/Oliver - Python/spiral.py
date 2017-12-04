"""
--- Day 3: Spiral Memory ---

You come across an experimental new kind of memory stored on an infinite
two-dimensional grid.
Each square on the grid is allocated in a spiral pattern starting at a location
marked 1 and then counting up while spiraling outward. For example, the first
few squares are allocated like this:

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...

While this is very space-efficient (no squares are skipped), requested data must
be carried back to square 1 (the location of the only access port for this
memory system) by programs that can only move up, down, left, or right. They
always take the shortest path: the Manhattan Distance between the location of
the data and square 1.

For example:
    Data from square 1 is carried 0 steps, since it's at the access port.
    Data from square 12 is carried 3 steps, such as: down, left, left.
    Data from square 23 is carried only 2 steps: up twice.
    Data from square 1024 must be carried 31 steps.

How many steps are required to carry the data from the square identified in your
puzzle input all the way to the access port?
"""

from math import sqrt, floor, ceil

raw = 347991
# raw = 9

# Part one
def get_band(number):
    # on which band of the spiral around nr 1 is the input nr
    return ceil((sqrt(number) - 1) / 2)

def band_range(band):
    # show full sequence of a specific band of the spiral
    lower = (((band * 2) - 1) ** 2) + 1
    upper = (((band * 2) + 1) ** 2) +1
    return list(range(lower, upper))

def distance_sequence(band):
    # show full distance sequence
    # start sequence from bottom right of spiral
    if band == 1:
        seq = list(range(2 * band - 1, band + 2))
        return seq * 4
    elif band != 1:
        seq = list(range(band * 2 - 1, band - 1, -1)) + list(range(band + 1, band * 2 + 1, 1))
        return seq * 4

def get_distance(raw):
    # make dictonary of numbers on band and distance from those numbers.
    # Then filter on the raw number
    seq_dict = dict(zip(band_range(get_band(raw)), distance_sequence(get_band(raw))))
    return seq_dict[raw]

# Shoot
get_band(raw)
band_range(get_band(raw))
distance_sequence(get_band(raw))
get_distance(raw)


