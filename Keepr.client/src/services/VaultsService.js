import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"
import { profilesService } from "./ProfilesService"

class VaultsService {
    async vaultCreate(vaultData) {
        if (vaultData.isPrivate == true) {
            vaultData.isPrivate = true
        } else {
            vaultData.isPrivate = true
        }
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.push(res.data)
        AppState.profileVaults.unshift(res.data)

    }


    async getProfileVaults() {
        const res = await api.get('account/vaults')
        AppState.profileVaults = res.data
    }

    async activeVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = res.data
    }

    async getVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = res.data
    }

    async getKeepFromVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.keeps = res.data
    }

    async deleteVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        // logger.log(res.data)
        AppState.vaults = AppState.vaults.filter(vault => vault.id !== vaultId)
    }
}


export const vaultsService = new VaultsService()