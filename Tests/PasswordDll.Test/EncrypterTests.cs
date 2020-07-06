using System;
using System.Diagnostics;
using System.Threading;
using EncrypterDll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasswordDll.Test
{
	[TestClass]
	public class EncrypterTests : IDisposable
	{
		[TestInitialize]
		public void Init()
		{
			Debug.WriteLine($"TestInitialize {DateTime.Now}");
		}

		[TestCleanup]
		public void Cleanup()
		{
			Debug.WriteLine($"TestCleanup {DateTime.Now}");
		}

		[TestMethod]
		public void Encrypt_abc()
		{
			#region Подготавливаем исходные данные для тестирования

			var input_str = "abc";
			var key = 10;
			var expected_result = "u128 u128 u128 ";
			#endregion

			#region Тестируем целевой объект
			var actual_result = EncrypterLite.Encrypt(input_str, key);
			#endregion

			#region Проверяем результаты теста
			Assert.AreEqual(expected_result, actual_result, "Ошибка кодирования строки");
			#endregion
		}

		[TestMethod, Timeout(10000), Description("Это метод тестирует...")]
		public void Encrypt_EmptyString()
		{
			var input = "";
			var key = 1;
			var expected = "";
			//System.Threading.Thread.Sleep(20000);
			var actual = EncrypterLite.Encrypt(input, key);

			Assert.AreEqual(expected, actual);

			//StringAssert.Contains();
			//CollectionAssert.Contains();
		}

		//[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		//public void Encrypt_NullRef()
		//{
		//	string input = null;
		//	var key = 1;

		//	var actual = Encrypter.Encrypt(input, key);

		//	Assert.IsNull(actual);
		//}

		public void Dispose()
		{
			Debug.WriteLine($"Dispose {DateTime.Now}");
		}
	}
}
