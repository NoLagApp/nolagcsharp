using NoLagClient.Communication;
using System.Collections.Generic;
using NoLagClient.Shared.Utilities;

namespace NoLagClient.Shared;

public class Topic
{
    private Client _client;
    private string _topicName;
    private IEnumerable<string> _identifiers;
    
    private Func<byte[], List<string>[], string, Task> _callbackFn;

    public Topic(Client client, string topicName, List<string> identifiers)
    {
        _client = client;
        _topicName = topicName;
        _identifiers = identifiers;
    }

    private void SaveIdentifiers(IEnumerable<string> newIdentifiers)
    {
        // if we see any new identifier, save it 
        IEnumerable<string> diff = newIdentifiers.Except<string>(_identifiers);
        _identifiers = _identifiers.Concat(diff);
    }

    private Task SubcribeToIdentifiersForTopicAsync(IEnumerable<string> newIdentifiers)
    {
        byte[] subcribeTopicMessage =  UnitSeparator.ToUnitSeparator()
    }
    
}