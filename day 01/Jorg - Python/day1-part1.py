def calculateCaptcha(input):
    sum = int(input[0]) if input[0]==input[len(input)-1] else 0

    for i in range(0, len(input)-1):
        if input[i]==input[i+1]:
            sum += int(input[i])

    return sum

def main():
    with open('input.txt', 'r') as myfile:
        data = myfile.read()
        print(calculateCaptcha(data))

main()