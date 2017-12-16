package main.scala.y2017.Day15

import main.scala.TestBase

class Tests extends TestBase {
  test("Example 1.1") { Runner.run(65, 16807, 1, 8921, 48271, 1, 5) shouldBe 1 }
  test("Example 1.2") { Runner.run(65, 16807, 1, 8921, 48271, 1, 40000000) shouldBe 588 }
  test("Puzzle 1") { Runner.run(634, 16807, 1, 301, 48271, 1, 40000000) shouldBe 573 }
  test("Example 2.1") { Runner.run(65, 16807, 4, 8921, 48271, 8, 5) shouldBe 0 }
  test("Example 2.2") { Runner.run(65, 16807, 4, 8921, 48271, 8, 1056) shouldBe 1 }
  test("Example 2.3") { Runner.run(65, 16807, 4, 8921, 48271, 8, 5000000) shouldBe 309 }
  test("Puzzle 2") { Runner.run(634, 16807, 4, 301, 48271, 8, 5000000) shouldBe 294 }
}
