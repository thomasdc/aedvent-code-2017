def readInFile(fileName):
    data = ''

    with open(fileName, 'r') as myfile:
        data = myfile.read()

    data = data.split('\n')
    data = list(map(lambda x: x.split('\t'), data))
    data = list(map(lambda x: list(map(int, x)), data))

    return data


def isWholeDivision(a, b):
    if a > b:
        return (a / b == round(a / b), a/b)
    else:
        return (b / a == round(b / a), b/a)


def calculateDivisionDifference(row):
    for i in range(0, len(row)):
        for j in range(i+1, len(row)):
            division = isWholeDivision(row[i], row[j])
            if division[0]:
                return division[1]

def calculateChecksum(data):
    divList = list(map(calculateDivisionDifference, data))
    return sum(divList)


print(calculateChecksum(readInFile('input.txt')))
