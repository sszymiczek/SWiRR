using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class BlockableTeleportationProvider : TeleportationProvider
{
    /* This class exists to resolve following bug:
     * 1. player enters a wall, screen fades, movement is disabled
     * 2. player tries to teleport ahead. Teleport raycast sees it as a valid location
     * 3. teleport is not allowed, so it does not happen, but IS queued
     * 4. player leaves a wall moving back
     * 5. teleport is reactevated with queued action, so it teleports player ahead ignoring wall's restriction (this step is the one that should not be happening)
     */
    public override bool QueueTeleportRequest(TeleportRequest teleportRequest)
    {
        if (!isActiveAndEnabled) { return false; }
        return base.QueueTeleportRequest(teleportRequest);
    }
}
