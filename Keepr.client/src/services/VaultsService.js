import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { profilesService } from "./ProfilesService"

class VaultsService {
    async vaultCreate(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.unshift(res.data)

    }

    async activeVault(vaultId) {
        // const vault = await profilesService.getVaults(vaultId)
        // const keeps = await profilesService.getKeeps(vaultId)
        // AppState.activeVault = vault
        // AppState.keeps = keeps
        const vault = await this.getVaultId(vaultId)
        const keeps = await this.getKeepFromVault(vaultId)
        // AppState.activeVault = vault
        AppState.keeps = keeps
    }

    async getVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = res.data
    }

    async getKeepFromVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.activeKeep = res.data
    }

    async deleteVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        // logger.log(res.data)
        AppState.vaults = AppState.vaults.filter(vault => vault.id !== vaultId)
    }
}


export const vaultsService = new VaultsService()