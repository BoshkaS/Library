using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Authors
            var author1 = new Author { AuthorId = 1, Pseudonym = "Агата Крісті" };
            var author2 = new Author { AuthorId = 2, Pseudonym = "Стівен Кінг" };
            var author3 = new Author { AuthorId = 3, Pseudonym = "Валер`ян Підмогильний" };
            var author4 = new Author { AuthorId = 4, Pseudonym = "Марко Вовчок" };
            var author5 = new Author { AuthorId = 5, Pseudonym = "Фріда Мак-Фадден" };
            var author6 = new Author { AuthorId = 6, Pseudonym = "Григорій Квітка-Основ'яненко" };
            var author7 = new Author { AuthorId = 7, Pseudonym = "Віктор Домонтович" };
            var author8 = new Author { AuthorId = 8, Pseudonym = "Юрій Яновський" };
            var author9 = new Author { AuthorId = 9, Pseudonym = "Іван Франко" };
            var author10 = new Author { AuthorId = 10, Pseudonym = "Е. М. Ремарк" };
            var author11 = new Author { AuthorId = 11, Pseudonym = "Ребекка Ярос" };
            var author12 = new Author { AuthorId = 12, Pseudonym = "Тея Ґуанзон" };

            modelBuilder.Entity<Author>().HasData(
                author1, author2, author3, author4, author5, author6,
                author7, author8, author9, author10, author11, author12
            );


            // Seed Categories
            var category1 = new Category { CategoryId = 1, Name = "Художня книга" };
            var category2 = new Category { CategoryId = 2, Name = "Українська література" };
            var category3 = new Category { CategoryId = 3, Name = "Дитячя книга" };

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);

            // Seed Languages
            var language1 = new Language { LanguageId = 1, Name = "Українська" };
            var language2 = new Language { LanguageId = 2, Name = "Англійська" };
            var language3 = new Language { LanguageId = 3, Name = "Німецька" };
            var language4 = new Language { LanguageId = 4, Name = "Французька" };

            modelBuilder.Entity<Language>().HasData(language1, language2, language3, language4);

            // Seed PublihingHouses
            var publisher1 = new PublishingHouse { PublishingHouseId = 1, Name = "Vivat" };
            var publisher2 = new PublishingHouse { PublishingHouseId = 2, Name = "КСД" };
            var publisher3 = new PublishingHouse { PublishingHouseId = 3, Name = "Видавництво" };
            var publisher4 = new PublishingHouse { PublishingHouseId = 4, Name = "Наш Формат" };
            var publisher5 = new PublishingHouse { PublishingHouseId = 5, Name = "Видавництво Старого Лева" };
            var publisher6 = new PublishingHouse { PublishingHouseId = 6, Name = "Лабораторія" };
            var publisher7 = new PublishingHouse { PublishingHouseId = 7, Name = "Ранок" };
            var publisher8 = new PublishingHouse { PublishingHouseId = 8, Name = "ArtHuss" };

            modelBuilder.Entity<PublishingHouse>().HasData(
                publisher1, publisher2, publisher3, publisher4,
                publisher5, publisher6, publisher7, publisher8
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 3,
                    Title = "Місто",
                    Description = "це історія амбітного юнака, який переїжджає із села у Київ",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/357_1_2.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 0,
                    NumberOfLikes = 5,
                    YearOfPublication = 2022,
                    LanguageId = 1,
                    CategoryId = 2,
                    PublishingHouseId = 2,
                    ISBN = "9786175511961",
                    CreatedDate = DateTime.Parse("2024-04-27T22:02:17.881533+03:00"),
                },
                new Book
                {
                    BookId = 4,
                    Title = "Вбивство у «Східному експресі»",
                    Description = "Знаменитий бельгійський детектив Еркюль Пуаро мусить терміново виїхати до Британії",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/181_1_2.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 0,
                    NumberOfLikes = 7,
                    YearOfPublication = 2022,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 2,
                    ISBN = "9786171298545",
                    CreatedDate = DateTime.Parse("2024-04-27T22:04:23.183974+03:00"),
                },
                new Book
                {
                    BookId = 5,
                    Title = "Тіло в бібліотеці",
                    Description = "Будинок сім’ї Бентрі сколихнула нечувана подія",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/61585_122279_cr.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 0,
                    NumberOfLikes = 2,
                    YearOfPublication = 2022,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 2,
                    ISBN = "9786171506145",
                    CreatedDate = DateTime.Parse("2024-04-27T22:05:07.617713+03:00"),
                },
                new Book
                {
                    BookId = 7,
                    Title = "Долорес Клейборн",
                    Description = "Коли Віра Донован, одна з найбагатших і найнепривітніших мешканок острова Літл-Тол у штаті Мен",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/img655_34.jpg",
                    NumberOfBorrows = 3,
                    NumberOfComments = 0,
                    NumberOfLikes = 0,
                    YearOfPublication = 2022,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 2,
                    ISBN = "97861715-05117",
                    CreatedDate = DateTime.Parse("2024-04-27T22:11:27.921312+03:00")
                },
                new Book
                {
                    BookId = 10,
                    Title = "Служниця",
                    Description = "Міллі Келловей втомилася жити в автівці...",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/108b83d0301ad8b444dd7c778476d19c.jpg",
                    NumberOfBorrows = 3,
                    NumberOfComments = 3,
                    NumberOfLikes = 2,
                    YearOfPublication = 2023,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 1,
                    ISBN = "9786171701427",
                    CreatedDate = DateTime.Parse("2024-05-22T11:01:55.577233+03:00")
                },
                new Book
                {
                    BookId = 11,
                    Title = "Конотопська відьма",
                    Description = "Вперше за останні сто років проза Григорія Квітки-Основ’яненка...",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/c43a4f3652d6231eed082a2e2a6e9635.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 2,
                    NumberOfLikes = 2,
                    YearOfPublication = 2023,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 1,
                    ISBN = "978617170114-4",
                    CreatedDate = DateTime.Parse("2024-05-24T23:52:41.584689+03:00")
                },
                new Book
                {
                    BookId = 13,
                    Title = "Невеличка драма",
                    Description = "роман про нові взаємини, новий побут",
                    BookImage = "https://mylibrarykursova.s3.eu-north-1.amazonaws.com/21e7537d8823fef840dac9ce74779c66.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 2,
                    NumberOfLikes = 4,
                    YearOfPublication = 2022,
                    LanguageId = 1,
                    CategoryId = 2,
                    PublishingHouseId = 1,
                    ISBN = "9786171701106",
                    CreatedDate = DateTime.Parse("2025-03-12T16:57:59.127562+02:00")
                },
                new Book
                {
                    BookId = 14,
                    Title = "І не лишилось жодного",
                    Description = "Десятеро незнайомців опинилися на далекому Солдатському острові...",
                    BookImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1746443192/library-books/blb2zjlg3gj8dsyhqgrq.jpg",
                    NumberOfBorrows = 1,
                    NumberOfComments = 1,
                    NumberOfLikes = 1,
                    YearOfPublication = 2023,
                    LanguageId = 1,
                    CategoryId = 2,
                    PublishingHouseId = 2,
                    ISBN = "9786171500242",
                    CreatedDate = DateTime.Parse("2025-05-05T14:07:40.416803+03:00")
                },
                new Book
                {
                    BookId = 15,
                    Title = "У напрямку до нуля",
                    Description = "Як між собою можуть бути пов’язані невдала спроба самогубства...",
                    BookImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1746565939/library-books/hwaxgkaaylrs2vwquxpd.jpg",
                    NumberOfBorrows = 0,
                    NumberOfComments = 0,
                    NumberOfLikes = 0,
                    YearOfPublication = 2025,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 2,
                    ISBN = "9786171513150",
                    CreatedDate = DateTime.MinValue // або DateTime.Parse("-infinity") не підтримується, треба змінити
                },
                new Book
                {
                    BookId = 16,
                    Title = "Таємниця семи циферблатів",
                    Description = "Джері Вейд проводить час в орендованому будинку...",
                    BookImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1746647844/library-books/cd8vpb9hqydqi9kplhde.jpg",
                    NumberOfBorrows = 1,
                    NumberOfComments = 0,
                    NumberOfLikes = 2,
                    YearOfPublication = 2025,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 2,
                    ISBN = "9786171513372",
                    CreatedDate = DateTime.Parse("2025-05-07T22:58:10.292266+03:00")
                },
                new Book
                {
                    BookId = 17,
                    Title = "Чому не Еванс?",
                    Description = "Небезпека може вигулькнути майже будь-де...",
                    BookImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1746648281/library-books/v8ovh96qtqrf6iqvubrc.jpg",
                    NumberOfBorrows = 2,
                    NumberOfComments = 0,
                    NumberOfLikes = 2,
                    YearOfPublication = 2025,
                    LanguageId = 1,
                    CategoryId = 1,
                    PublishingHouseId = 5,
                    ISBN = "9786171283480",
                    CreatedDate = DateTime.Parse("2025-05-07T23:04:47.903710+03:00")
                }
            );

            // Seed BookCopies
            modelBuilder.Entity<BookCopy>().HasData(
                new BookCopy { BookCopyId = 12, BookId = 3 },
                new BookCopy { BookCopyId = 13, BookId = 3 },
                new BookCopy { BookCopyId = 14, BookId = 14 },
                new BookCopy { BookCopyId = 15, BookId = 3 },
                new BookCopy { BookCopyId = 16, BookId = 4 },
                new BookCopy { BookCopyId = 17, BookId = 5 },
                new BookCopy { BookCopyId = 18, BookId = 7 },
                new BookCopy { BookCopyId = 19, BookId = 10 },
                new BookCopy { BookCopyId = 20, BookId = 11 },
                new BookCopy { BookCopyId = 21, BookId = 13 },
                new BookCopy { BookCopyId = 22, BookId = 14 },
                new BookCopy { BookCopyId = 23, BookId = 11 },
                new BookCopy { BookCopyId = 24, BookId = 15 },
                new BookCopy { BookCopyId = 25, BookId = 16 },
                new BookCopy { BookCopyId = 26, BookId = 17 },
                new BookCopy { BookCopyId = 27, BookId = 10 },
                new BookCopy { BookCopyId = 28, BookId = 10 },
                new BookCopy { BookCopyId = 29, BookId = 3 },
                new BookCopy { BookCopyId = 30, BookId = 3 }
            );

            // Seed AuthorBookDatas
            modelBuilder.Entity<AuthorBook>().HasData(
                new AuthorBook { AuthorBookId = 1, AuthorId = 3, BookId = 2 },
                new AuthorBook { AuthorBookId = 2, AuthorId = 3, BookId = 3 },
                new AuthorBook { AuthorBookId = 3, AuthorId = 1, BookId = 4 },
                new AuthorBook { AuthorBookId = 4, AuthorId = 1, BookId = 5 },
                new AuthorBook { AuthorBookId = 5, AuthorId = 1, BookId = 6 },
                new AuthorBook { AuthorBookId = 6, AuthorId = 2, BookId = 7 },
                new AuthorBook { AuthorBookId = 7, AuthorId = 2, BookId = 8 },
                new AuthorBook { AuthorBookId = 8, AuthorId = 5, BookId = 10 },
                new AuthorBook { AuthorBookId = 9, AuthorId = 6, BookId = 11 },
                new AuthorBook { AuthorBookId = 10, AuthorId = 3, BookId = 13 },
                new AuthorBook { AuthorBookId = 11, AuthorId = 1, BookId = 14 },
                new AuthorBook { AuthorBookId = 12, AuthorId = 1, BookId = 15 },
                new AuthorBook { AuthorBookId = 13, AuthorId = 1, BookId = 16 },
                new AuthorBook { AuthorBookId = 14, AuthorId = 1, BookId = 17 }
            );

            // Seed Roles
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = 1,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "1fe6b44d-4db2-44e3-b52b-f1e525366335"
                },
                new AppRole
                {
                    Id = 2,
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "08305483-2ba2-41dd-8f60-5eb892c319f9"
                }
            );
        }
    }
}
