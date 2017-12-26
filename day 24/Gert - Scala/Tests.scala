package main.scala.y2017.Day24

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day24/example.txt").getLines().toArray
    Runner.run(input) shouldBe 31
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day24/puzzle.txt").getLines().toArray
    Runner.run(input) shouldBe 1940
  }

  test("Example 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day24/example.txt").getLines().toArray
    Runner.run2(input) shouldBe 19
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day24/puzzle.txt").getLines().toArray
    Runner.run2(input) shouldBe 1928
  }
}
