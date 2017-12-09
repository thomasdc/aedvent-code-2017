package main.scala.y2017.Day08

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day08/example.txt").getLines()
    Runner.run(input.toList) shouldBe 1
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day08/puzzle.txt").getLines()
    Runner.run(input.toList) shouldBe 6828
  }
  test("Example 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day08/example.txt").getLines()
    Runner.run(input.toList, part2 = true) shouldBe 10
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day08/puzzle.txt").getLines()
    Runner.run(input.toList, part2 = true) shouldBe 7234
  }
}
