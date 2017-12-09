package main.scala.y2017.Day04

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Example 1") { Runner.run("aa bb cc dd ee") shouldBe true }
  test("Example 2") { Runner.run("aa bb cc dd aa") shouldBe false }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day04/puzzle.txt").getLines()
    Runner.run(input) shouldBe 466
  }

  test("Example 2.1") { Runner.run2("abcde fghij") shouldBe true }
  test("Example 2.2") { Runner.run2("abcde xyz ecdab") shouldBe false }
  test("Example 2.3") { Runner.run2("a ab abc abd abf abj") shouldBe true }
  test("Example 2.4") { Runner.run2("iiii oiii ooii oooi oooo") shouldBe true }
  test("Example 2.5") { Runner.run2("oiii ioii iioi iiio") shouldBe false }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day04/puzzle.txt").getLines()
    Runner.run2(input) shouldBe 251
  }
}
