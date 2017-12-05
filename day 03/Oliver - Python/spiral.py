from math import sqrt, ceil

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


