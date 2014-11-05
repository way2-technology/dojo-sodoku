using NUnit.Framework;

namespace Sodoku.Tests {
    public class SolverTests {
        public const string Board = @"+-------+-------+-------+
                                      | _ 6 _ | 1 _ 4 | _ 5 _ |
                                      | _ _ 8 | 3 _ 5 | 6 _ _ |
                                      | 2 _ _ | _ _ _ | _ _ 1 |
                                      +-------+-------+-------+
                                      | 8 _ _ | 4 _ 7 | _ _ 6 |
                                      | _ _ 6 | _ _ _ | 3 _ _ |
                                      | 7 _ _ | 9 _ 1 | _ _ 4 |
                                      +-------+-------+-------+
                                      | 5 _ _ | _ _ _ | _ _ 2 |
                                      | _ _ 7 | 2 _ 6 | 9 _ _ |
                                      | _ 4 _ | 5 _ 8 | _ 7 _ |
                                      +-------+-------+-------+";

        private Solver _solver;

        [SetUp]
        public void SetUp() {
            _solver = new Solver(Board);
        }

        [Test]
        public void Deve_inicializar_solver_9x9() {
            Assert.AreEqual(9, _solver.Board.Length);
            Assert.AreEqual(9, _solver.Board[0].Length);
        }

        [Test]
        public void Deve_inicializar_preenchidos() {
            Assert.AreEqual(6, _solver.Board[0][1]);
            Assert.AreEqual(1, _solver.Board[5][5]);
        }

        [Test]
        public void Deve_inicializar_nao_preenchidos_com_zero() {
            Assert.AreEqual(0, _solver.Board[0][0]);
            Assert.AreEqual(0, _solver.Board[8][8]);
        }

        [Test]
        public void Deve_retornar_linha_0() {
            CollectionAssert.AreEqual(new[] { 0, 6, 0, 1, 0, 4, 0, 5, 0 }, _solver.GetRow(0));
        }

        [Test]
        public void Deve_retornar_coluna_0() {
            CollectionAssert.AreEqual(new[] { 0, 0, 2, 8, 0, 7, 5, 0, 0 }, _solver.GetColumn(0));
        }

        [TestCase(0, new[] { 0, 6, 0, 0, 0, 8, 2, 0, 0 })]
        [TestCase(1, new[] { 1, 0, 4, 3, 0, 5, 0, 0, 0 })]
        [TestCase(4, new[] { 4, 0, 7, 0, 0, 0, 9, 0, 1 })]
        [TestCase(8, new[] { 0, 0, 2, 9, 0, 0, 0, 7, 0 })]
        public void Deve_retornar_caixa(int box, int[] esperado) {
            CollectionAssert.AreEqual(esperado, _solver.GetBox(box));
        }

        [TestCase(0, 0, new[] { 3, 9 })]
        [TestCase(0, 2, new[] { 3, 9 })]
        public void Deve_encontrar_os_valores_validos_para_uma_posicao(int row, int column, int[] esperado) {
            CollectionAssert.AreEqual(esperado, _solver.GetPossibleValues(row, column));
        }

        [Test]
        public void Deve_resolver() {
            Assert.IsTrue(_solver.Solve());
        }
    }
}
