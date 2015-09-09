using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Data.Repositories.Initialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new ApplicationContextInitializer());

            using (var context = new ApplicationDbContext())
            {
                
                context.Database.Initialize(force: true);
            }

            var users = new List<string>();
            var articles = new List<int>();
            var tags = new List<Tag>();

            using (var context = new ApplicationDbContext())
            {
                users.AddRange(context.Users.Select(x => x.Id));
               // articles.AddRange(context.Articles.Select(x => x.Id));
                tags.AddRange(context.Tags);
                context.Database.Connection.Close();
            }
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            GenerateArticles(users);
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Done with article Generation time {0}", elapsedTime);


            using (var context = new ApplicationDbContext())
            {
                articles.AddRange(context.Articles.Select(x => x.Id));
            }

            GenerateComments(articles, users);

            GenerateClicks(articles, users);

        }

        private static void GenerateArticles(List<string> users)
        {
            var random = new Random();

            Parallel.For(0, 3000, new ParallelOptions {MaxDegreeOfParallelism = 30}, index =>
            {
                using (var context = new ApplicationDbContext())
                {
                    var articles = new List<Article>();
                    var tags = context.Tags.ToList();
                    for (var i = 0; i < 10; i++)
                    {
                        var article = new Article();
                        article.Tags.Add(tags.Random());
                        article.Tags.Add(tags.Random());
                        article.Header = SeedExtensions.Headers.Random();
                        article.TeaserText = SeedExtensions.TeaserTexts.Random();
                        article.Body = SeedExtensions.Body;
                        article.CreatedDate = DateTime.UtcNow.AddDays(0 - random.Next(5, 100));
                        article.IsPublished = random.Next(0, 100) > 15;
                        article.AuthorId = users.Random();
                        article.Stamp = Guid.NewGuid();
                        articles.Add(article);
                    }
                    ((DbSet<Article>) context.Articles).AddRange(articles);
                    try
                    {
                        Console.WriteLine("Saving categories index {0}, count {1}", index, articles.Count());
                        context.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR Proccessing Clicks for ArticleId: {0}", index);
                        Console.WriteLine(exception.Message);
                        if (exception.InnerException != null)
                        {
                            Console.WriteLine(exception.InnerException.Message);
                        }
                        Console.ResetColor();
                    }
                }
            });
        }

        private static void GenerateClicks(List<int> articles, List<string> users)
        {
            Parallel.ForEach(articles, new ParallelOptions { MaxDegreeOfParallelism = 30 }, articleId =>
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var random = new Random();
                var viewCount = random.Next(50, 1000);
                var acrticlesView = new List<StatiscticArticleView>();
                Console.WriteLine("Proccessing Clicks for ArticleId: {0}, Clicks count: {1}", articleId, viewCount);
                for (int i = 0; i < viewCount; i++)
                {
                    acrticlesView.Add(new StatiscticArticleView
                    {
                        ArticleId = articleId,
                        Time = DateTime.UtcNow.AddDays(0 - random.Next(5, 100)).AddHours(0 - random.Next(0, 24)).AddMinutes(0 - random.Next(0, 60)),
                        UserId = random.Next(0, 100) > 15 ? users.Random() : null
                    });
                }

                try
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var commentEnity = (DbSet<StatiscticArticleView>)context.StatiscticArticleViews;
                        commentEnity.AddRange(acrticlesView);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR Proccessing Clicks for ArticleId: {0}", articleId);
                    Console.WriteLine(exception.Message);
                    if (exception.InnerException != null)
                    {
                        Console.WriteLine(exception.InnerException.Message);
                    }
                    Console.ResetColor();
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Done with article id {0} time spend {1}", articleId, elapsedTime);
            });
        }

        private static void GenerateComments(List<int> articles, List<string> users)
        {
            Parallel.ForEach(articles,new ParallelOptions{ MaxDegreeOfParallelism = 30} , articleId =>
            {
                var commentsList = new List<Comment>();
                var random = new Random();
                var viewCount = random.Next(5, 60);
                Console.WriteLine("Proccessing Comments ArticleId: {0}, Comments Count: {1}", articleId, viewCount);
                for (int i = 0; i < viewCount; i++)
                {
                    var comment = new Comment
                    {
                        ArticleId = articleId,
                        Stamp = Guid.NewGuid(),
                        CreatedDate =
                            DateTime.UtcNow.AddDays(0 - random.Next(5, 100))
                                .AddHours(0 - random.Next(0, 24))
                                .AddMinutes(0 - random.Next(0, 60)),
                        AuthorId = users.Random(),
                        Body = SeedExtensions.Headers.Random() + " " + SeedExtensions.Headers.Random()
                    };
                    commentsList.Add(comment);
                }

                try
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var commentEnity = (DbSet<Comment>) context.Comments;
                        commentEnity.AddRange(commentsList);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR Proccessing Comments ArticleId: {0}", articleId);
                    Console.WriteLine(exception.Message);
                    if (exception.InnerException != null)
                    {
                        Console.WriteLine(exception.InnerException.Message);
                    }
                    Console.ResetColor();
                }
            });
        }
    }
}
