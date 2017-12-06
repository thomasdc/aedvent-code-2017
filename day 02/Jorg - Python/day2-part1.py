def calculateMaxMin(row):
    min=row[0]
    max=row[0]

    for i in range(1, len(row)):
        if min > row[i]:
            min = row[i]
        if max < row[i]:
            max=row[i]

    return (int(max), int(min))

def readInFile(fileName):
    data = ''

    with open(fileName, 'r') as myfile:
        data = myfile.read()

    data = data.split('\n')
    data = list(map(lambda x: x.split('\t'), data))
    data = list(map(lambda x: list(map(int, x)), data))

    return data

def calculateMaxMinDifference(row):
    maxMin = calculateMaxMin(row)
    return maxMin[0]-maxMin[1]

def calculateChecksum(data):
    maxMinList = list(map(calculateMaxMinDifference, data))
    return sum(maxMinList)

print(calculateChecksum(readInFile('input.txt')))