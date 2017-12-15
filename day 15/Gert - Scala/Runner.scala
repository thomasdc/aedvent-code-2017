package main.scala.y2017.Day15

object Runner {
  def run(aSeed: Int, aMultiplier: BigInt, aCriteria: Int,
          bSeed: Int, bMultiplier: BigInt, bCriteria: Int, numCompares: Int): Int = {
    val genA = new Generator(aSeed, aMultiplier, aCriteria)
    val genB = new Generator(bSeed, bMultiplier, bCriteria)
    compare(genA, genB, numCompares)
  }

  def compare(genA: Generator, genB: Generator, numCompares: Int): Int = {
    (1 to numCompares)
      .map(_ => judge(genA.generate(), genB.generate()))
      .count(matching => matching)
  }

  def judge(a: BigInt, b: BigInt): Boolean =
    a.toString(2).takeRight(16) == b.toString(2).takeRight(16)
}
