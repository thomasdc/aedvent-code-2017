package main.scala.y2017.Day21

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    Runner.run(Array("../.# => ##./#../...", "" +
      ".#./..#/### => #..#/..../..../#..#"), 2) shouldBe 12
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day21/puzzle.txt").getLines().toArray
    Runner.run(input, 5) shouldBe 205
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day21/puzzle.txt").getLines().toArray
    Runner.run(input, 18) shouldBe 3389823
  }
}