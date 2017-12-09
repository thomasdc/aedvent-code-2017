package main.scala.y2017.Day07

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day07/example.txt").getLines()
    Runner.run(input) shouldBe "tknk"
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day07/puzzle.txt").getLines()
    Runner.run(input) shouldBe "veboyvy"
  }

  test("Example 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day07/example.txt").getLines()
    Runner.run2(input) shouldBe 60
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day07/puzzle.txt").getLines()
    Runner.run2(input) shouldBe 749
  }
}
