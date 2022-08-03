import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService {

    async addKeepToVault(vaultKeep) {
        const res = await api.post('api/vaultkeeps', vaultKeep)
    }

    async removeKeep(vaultKeepId, keepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        AppState.keeps = AppState.keeps.filter(keep => keep.id !== keepId)
    }


}


export const vaultKeepsService = new VaultKeepsService()