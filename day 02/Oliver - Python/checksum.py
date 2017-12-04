# Part one


def read_input(path):
    with open(path, "r") as a:
        return [list(map(int, line.split())) for line in a]


def cumsum(matrix):
    return sum(max(line) - min(line) for line in matrix)


raw = read_input("day 02/Oliver - Python/input.txt")
cumsum(raw)

# Part two
# find the only two numbers in each row where one evenly divides the other


def checksum(matrix):
    cumsum = 0
    for line in matrix:
        for x in range(0, len(line)):
            for y in range(0, len(line)):
                if line[x] % line[y] == 0 and x != y:
                    cumsum += (line[x] // line[y])
    return cumsum


checksum(raw)

