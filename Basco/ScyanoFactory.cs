namespace Basco
{
    using Scyano;
    using Scyano.Core;
    using Scyano.Tasks;

    public class ScyanoFactory
    {
        public static IScyano Create()
        {
            //// TODO: move to scyano
            var messageConsumer = new MessageConsumerRetriever();
            var queueController = new MessageQueueController();
            var taskExecutor = new ScyanoTaskExecutor(new ScyanoTokenSource(), new ScyanoFireAndForgetTask());
            var task = new ScyanoFireAndForgetTask();
            var dequeue = new DequeueTask();
            return new Scyano(messageConsumer, queueController, taskExecutor, task, dequeue);
        }
    }
}