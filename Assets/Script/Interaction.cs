public interface Interaction
{
    EInteract ToInteract();
}

public enum EInteract
{
    NONE,
    COMPUTER,
    TORCH,
    OBJECT_DYNAMIC
}