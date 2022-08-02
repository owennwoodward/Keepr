import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getMyProfile(id) {
        const res = await api.get('api/profiles/' + id)
        AppState.profile = res.data
    }
    async getKeeps(id) {
        const res = await api.get('api/profiles/' + id + '/keeps')
        AppState.keeps = res.data
    }
    async getVaults(id) {
        const res = await api.get('api/profiles/' + id + '/vaults')
        logger.log(res.data, 'vaults')
        AppState.vaults = res.data
    }
}


export const profilesService = new ProfilesService()