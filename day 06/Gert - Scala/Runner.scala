package main.scala.y2017.Day06

object Runner extends Runner {}

class Runner {
  var seenConfigurations = scala.collection.mutable.MutableList[List[Int]]()

  def run(memoryBanks: List[Int], returnCycleSize: Boolean = false): Int = {
    var memoryBankUnderConstruction = memoryBanks
    while(!seenConfigurations.contains(memoryBankUnderConstruction)) {
      seenConfigurations += memoryBankUnderConstruction
      memoryBankUnderConstruction = redistribute(memoryBankUnderConstruction)
    }

    if(returnCycleSize)
      seenConfigurations.size - seenConfigurations.toList.indexOf(memoryBankUnderConstruction)
    else
      seenConfigurations.size
  }

  def redistribute(memoryBanks: List[Int]): List[Int] = {
    def largestMemoryBank = getLargestMemoryBank(memoryBanks)

    var toDistribute = memoryBanks(largestMemoryBank)
    val newConfiguration = memoryBanks.indices
      .map(i => {
        val remainingMemoryBanksToFill = memoryBanks.length - i
        val currentMemoryBankToFill = (i + largestMemoryBank + 1) % memoryBanks.length
        val currentAdditive = Math.ceil(toDistribute.toDouble / remainingMemoryBanksToFill).toInt

        toDistribute = toDistribute - currentAdditive

        val currentValue =
          if(currentMemoryBankToFill == largestMemoryBank) 0
          else memoryBanks(currentMemoryBankToFill)
        currentValue + currentAdditive
      })
      .toList

    shiftLeft(newConfiguration, (memoryBanks.length - 1) - largestMemoryBank).toList
  }

  def getLargestMemoryBank(memoryBanks: List[Int]): Int = memoryBanks.indexOf(memoryBanks.max)

  def shiftLeft(memoryBanks: List[Int], rotation: Int): List[Int] = {
    memoryBanks.drop(rotation) ++ memoryBanks.take(rotation)
  }
}
