import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getAll() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
    }
    async setActive(id) {
        const res = await api.get('api/keeps/' + id)
        AppState.activeKeep = res.data
    }

    async keepCreate(keepData) {
        const res = await api.post('api/keeps', keepData)
        AppState.vaults.unshift(res.data)

    }

    async deleteKeep() {
        await api.delete(`api/keeps/ ${AppState.activeKeep.id}`)
        AppState.keeps = AppState.keeps.filter(keep => keep.id !== AppState.activeKeep.id)
        AppState.activeKeep = {}
    }

}


export const keepsService = new KeepsService()