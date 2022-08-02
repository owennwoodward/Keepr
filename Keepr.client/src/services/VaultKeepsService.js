import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

    async addToVault(vaultId) {
        const vk = {}
        vk.vaultId = vaultId
        vk.keepId = AppState.activeKeep.id
        await api.post('api/vaultkeeps', vk)
        AppState.activeKeep.keeps = AppState.activeKeep.keeps + 1
        logger.log(AppState.activeKeep.keeps)
    }

}


export const vaultKeepsService = new VaultKeepsService()