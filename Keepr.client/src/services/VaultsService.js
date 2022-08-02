import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async vaultCreate(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.unshift(res.data)

    }
}


export const vaultsService = new VaultsService()