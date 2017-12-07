def calculateCaptcha(input, distance):
    sum=0

    for i in range(0, len(input)):
        if input[i]==input[(i+distance) % len(input)]:
            sum += int(input[i])

    return sum

def main():
    data = ''

    with open('input.txt', 'r') as myfile:
        data = myfile.read()

    print(calculateCaptcha(data, 1))
    print(calculateCaptcha(data, int(len(data)/2)))

main()