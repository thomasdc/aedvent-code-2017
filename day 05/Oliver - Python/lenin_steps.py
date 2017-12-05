"""
(0) 3  0  1  -3  - before we have taken any steps.
(1) 3  0  1  -3  - jump with offset 0 (that is, don't jump at all). Fortunately, the instruction is then incremented to 1.
 2 (3) 0  1  -3  - step forward because of the instruction we just modified. The first instruction is incremented again, now to 2.
 2  4  0  1 (-3) - jump all the way to the end; leave a 4 behind.
 2 (4) 0  1  -2  - go back to where we just were; increment -3 to -2.
 2  5  0  1  -2  - jump 4 steps forward, escaping the maze.

"""

def read_input(path):
    with open(path, "r") as a:
        return list(map(int, [line.strip() for line in a]))

# After each jump, if the offset was three or more, instead decrease it by 1. Otherwise, increase it by 1 as before.

def lenin_steps_two(input):
    # initialise
    increment_position = 0
    value_forward = 0
    step = 0
    # get position where to increment
    while increment_position < len(input):
        step +=1
        # increment_position += value_forward
        value_forward = input[increment_position]
        if value_forward >=3:
            input[increment_position] -= 1
        else:
            input[increment_position] += 1
        increment_position += value_forward
    print(step)

# lenin = [0, 3, 0, 1, -3]
lenin = read_input("day 05/Oliver - Python/input.txt")
lenin_steps_two(lenin)