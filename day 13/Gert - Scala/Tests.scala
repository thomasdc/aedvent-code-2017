package main.scala.y2017.Day13

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    Runner.run(List(
      "0: 3",
      "1: 2",
      "4: 4",
      "6: 4",
      "7: 3"
    )) shouldBe 24
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day13/puzzle.txt").getLines().toList
    Runner.run(input) shouldBe 1704
  }
  test("Example 2") {
    Runner.run2(List(
      "0: 3",
      "1: 2",
      "4: 4",
      "6: 4"
    )) shouldBe 10
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day13/puzzle.txt").getLines().toList
    Runner.run2(input) shouldBe 3970918
  }
}
