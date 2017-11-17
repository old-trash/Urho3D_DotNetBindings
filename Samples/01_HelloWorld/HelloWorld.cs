using System;

class HelloWorld : Sample
{
    public HelloWorld(Context context) : base(context)
    {
    }
    
    public override void Start()
    {
        base.Start();
        CreateText();
        SubscribeToEvents();
        //Sample::InitMouseMode(MM_FREE);
    }
    
    void CreateText()
    {
    }

    void SubscribeToEvents()
    {
    }

    /*void HandleUpdate(StringHash eventType, VariantMap& eventData)
    {
    }*/
}
