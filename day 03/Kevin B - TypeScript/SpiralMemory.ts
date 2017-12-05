class SpiralMemoryPart1 {

    calculateAmountOfSteps(numberToGet: number): void {
        let lengthOfSquare: number = this.calculateLengthOfSquare(numberToGet);
        let cornerNumbers: number[] = this.calculateCornerNumbers(lengthOfSquare);
        let coordinates: number[] = this.getCoordinatesOfNumber(cornerNumbers, numberToGet, lengthOfSquare);
        let amountOfSteps: number = this.calculateSteps(coordinates);
        console.log(amountOfSteps);
    }

    private calculateLengthOfSquare(input: number): number {
        let lengthOfSquare: number = Math.sqrt(input) / 2.0;
        (lengthOfSquare.toString().slice(-2) === ".5") ? lengthOfSquare = Math.round(lengthOfSquare) - 1 : lengthOfSquare = Math.round(lengthOfSquare);
        return lengthOfSquare;
    }

    private calculateCornerNumbers(lengthOfSquare: number): number[] {
        let cornerNumbers: number[] = [];
        cornerNumbers.push(this.funkyCalculationForRightUpNumber(lengthOfSquare));
        for (let i=0; i < 3; i++) {
            cornerNumbers.push(cornerNumbers[i]+lengthOfSquare*2);
        }
        return cornerNumbers;
    }

    private funkyCalculationForRightUpNumber(lengthOfSquare: number) {
        let rightUpNumber: number = 1;
        let accumulator: number = 1;
        for (let i = 1; i <= lengthOfSquare; i++) {
            rightUpNumber += accumulator;
            accumulator += 8;
        }
        return rightUpNumber + lengthOfSquare;
    }

    private getCoordinatesOfNumber(cornerNumbers: number[], numberToGet: number, lengthOfSquare: number): number[] {
        let coordinates: number[] = [];
        let difference: number;
        if (numberToGet <= cornerNumbers[0]) {
            difference = cornerNumbers[0] - numberToGet;
            coordinates.push(lengthOfSquare);
            coordinates.push(lengthOfSquare-difference);
        } else if(numberToGet <= cornerNumbers[1]) {
            difference = cornerNumbers[1] - numberToGet;
            coordinates.push(-(lengthOfSquare-difference));
            coordinates.push(lengthOfSquare);
            console.log(coordinates)
        } else if (numberToGet <= cornerNumbers[2]) {
            difference = cornerNumbers[2] - numberToGet;
            coordinates.push(-lengthOfSquare);
            coordinates.push(-(lengthOfSquare-difference));
        } else if (numberToGet <= cornerNumbers[3]) {
            difference = cornerNumbers[3] - numberToGet;
            coordinates.push((lengthOfSquare-difference));
            coordinates.push(-lengthOfSquare);
        }
        return coordinates;
    }

    private calculateSteps(coordinates: number[]): number {
        return Math.abs(coordinates[0]) + Math.abs(coordinates[1]);
    }
}

new SpiralMemoryPart1().calculateAmountOfSteps(312051);