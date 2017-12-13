# Axial Coordinates
# See: https://www.redblobgames.com/grids/hexagons/#coordinates-axial
# function hex_distance(a, b):
#     return (abs(a.q - b.q)
#           + abs(a.q + a.r - b.q - b.r)
#           + abs(a.r - b.r)) / 2
# where B is the origin (0,0)

input = "se,sw,se,sw,sw"
path = list(input.split(','))

def read_input(path):
    return [line.replace('\n', '').split(',') for line in open(path)][0]

def moves(move):
    return {
        'n': (0,-1),
        'ne': (1,-1),
        'se': (1,0),
        's': (0,1),
        'sw': (-1,1),
        'nw': (-1,0),
    }[move]

def path_length(x, y):
    return (abs(x) + abs(x + y) + abs(y)) // 2


path = read_input('day 11/Oliver - Python/input.txt')
position = [(0,0)]
steps = 0
max_distance = 0
for step in path:
    steps += 1
    position.append(tuple(map(sum, zip(position[steps-1], moves(step)))))
    max_distance = max(max_distance, path_length(position[-1][0], position[-1][1]))

path_length(position[-1][0], position[-1][1])
