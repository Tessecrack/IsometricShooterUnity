public class SharedData
{
	public UserInput UserInput { get; private set; }
	public StaticData StaticData { get; private set; }
	public SceneData SceneData { get; private set; }
	public RuntimeData RuntimeData { get; private set; }

	public void Init(StaticData staticData, SceneData sceneData, RuntimeData runtimeData, UserInput userInput)
	{
		StaticData = staticData;
		SceneData = sceneData;
		RuntimeData = runtimeData;
		UserInput = userInput;
	}
}
