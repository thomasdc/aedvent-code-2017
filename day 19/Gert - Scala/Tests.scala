package main.scala.y2017.Day19

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day19/example.txt").mkString("")
    Runner.run(input) shouldBe "ABCDEF"
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day19/puzzle.txt").mkString("")
    Runner.run(input) shouldBe "KGPTMEJVS" // 16328
  }
}
