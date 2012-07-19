/// <summary>
/// A trigger that is activated when the attached object is clicked.
/// </summary>
public class ClickTrigger : ActivateTrigger
{
	/// <summary>
	/// An event received when the player clicks the object.
	/// </summary>
	public void Click()
	{
		DoActivateTrigger();
	}
}